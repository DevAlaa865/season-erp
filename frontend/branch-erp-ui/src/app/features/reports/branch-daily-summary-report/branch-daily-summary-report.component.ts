import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MasterDataService } from '../../../services/master-data.service';
import { BranchSalesDailyService } from '../../../services/branch-sales-daily.service';
import { ShortByTypePipe } from './short-by-type.pipe';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import * as ExcelJS from 'exceljs';
import { saveAs } from 'file-saver';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';

interface BranchShortageSummary {
  shortageTypeId: number;
  shortageTypeName: string;
  amount: number;
}

interface BranchDailySummaryRow {
  branchId: number;
  branchName: string;

  cashAmount: number;
  networkAmount: number;
  creditAmount: number;
  totalSales: number;
  grandTotal: number;
  difference: number;

  totalShortageAmount: number;
  shortages: BranchShortageSummary[];
}

interface BranchDailySummaryFilter {
  fromDate: string;
  toDate: string;
  cityId?: number | null;
  activityTypeId?: number | null;
  branchType: string;          // 'All' | 'Shop' | 'Kiosk'
  onlyWithShortage: boolean;
}

@Component({
  selector: 'app-branch-daily-summary-report',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ShortByTypePipe],
  templateUrl: './branch-daily-summary-report.component.html'
})
export class BranchDailySummaryReportComponent implements OnInit {
  
  form!: FormGroup;

  cities: any[] = [null];
  activityTypes: any[] = [null];

  rows: BranchDailySummaryRow[] = [];
  shortageTypes: { id: number; name: string }[] = [];

  isLoading = false;
  errorMessage = '';


pagedRows: BranchDailySummaryRow[] = [];
pageSize = 20;
currentPage = 1;
totalPages = 1;

  constructor(
    private fb: FormBuilder,
    private masterDataService: MasterDataService,
    private branchSalesDailyService: BranchSalesDailyService,
    private router:Router,
      private route: ActivatedRoute 
  ) {}

  ngOnInit(): void {
    this.buildForm();
    this.loadMasterData();
    
  }

  buildForm(): void {
    this.form = this.fb.group({
      fromDate: [null, Validators.required],
      toDate: [null, Validators.required],
      cityId : [null],
      activityTypeId: [null],
      branchType: ['All'],
      onlyWithShortage: [false]
    });

    const today = new Date().toISOString().split('T')[0];
    this.form.patchValue({
      fromDate: today,
      toDate: today
    });
  }

  loadMasterData(): void {
    this.masterDataService.getCities().subscribe({
      next: (res: any) => {
        this.cities = res.data || [];
      },
      error: () => {
        console.error('خطأ في تحميل المدن');
      }
    });

    this.masterDataService.getActivityTypes().subscribe({
      next: (res: any) => {
        this.activityTypes = res.data || [];
      },
      error: () => {
        console.error('خطأ في تحميل الأنشطة');
      }
    });
  }

  search(): void {
    this.errorMessage = '';

    if (this.form.invalid) {
      this.errorMessage = 'من فضلك أكمل بيانات الفترة أولاً.';
      return;
    }

    const raw = this.form.value;

    const filter: BranchDailySummaryFilter = {
      fromDate: raw.fromDate,
      toDate: raw.toDate,
      cityId: raw.cityId  || null,
      activityTypeId: raw.activityTypeId  || null,
      branchType: raw.branchType,
      onlyWithShortage: raw.onlyWithShortage
    };

    this.isLoading = true;
    this.rows = [];
    this.shortageTypes = [];

    this.branchSalesDailyService.getSummaryReport(filter).subscribe({
      next: (res: any) => {
        this.isLoading = false;
 

        if (!res || res.success === false) {
          this.errorMessage = res?.message || 'لم يتم العثور على بيانات للتقرير.';
          this.rows = [];
          this.shortageTypes = [];
          return;
        }

        this.rows = res.data || [];

        const map = new Map<number, string>();

        this.rows.forEach((row: any) => {
          (row.shortages || []).forEach((s: any) => {
            if (!map.has(s.shortageTypeId)) {
              map.set(s.shortageTypeId, s.shortageTypeName);
            }
          });
        });

        this.shortageTypes = Array.from(map.entries()).map(([id, name]) => ({ id, name }));
               this.currentPage = 1;
              this.calculatePagination();
      },
      error: () => {
        this.isLoading = false;
        this.errorMessage = 'حدث خطأ أثناء جلب بيانات التقرير.';
        this.rows = [];
        this.shortageTypes = [];
      }
    });
  }
openDailyDetails(row: BranchDailySummaryRow) {
  const date = this.form.get('fromDate')?.value;

  this.router.navigate(
    ['/reports/daily-sales-inquiry'],
    {
      queryParams: {
        branchId: row.branchId,
        salesDate: date
      }
    }
  );
}

calculatePagination(): void {
  if (!this.rows || this.rows.length === 0) {
    this.pagedRows = [];
    this.totalPages = 1;
    return;
  }

  this.totalPages = Math.ceil(this.rows.length / this.pageSize);

  const startIndex = (this.currentPage - 1) * this.pageSize;
  const endIndex = startIndex + this.pageSize;

  this.pagedRows = this.rows.slice(startIndex, endIndex);
}
trackByBranch(index: number, item: BranchDailySummaryRow) {
  return item.branchId;
}
goToPage(page: number): void {
  if (page < 1 || page > this.totalPages) return;
  this.currentPage = page;
  this.calculatePagination();
}

nextPage(): void {
  if (this.currentPage < this.totalPages) {
    this.currentPage++;
    this.calculatePagination();
  }
}

prevPage(): void {
  if (this.currentPage > 1) {
    this.currentPage--;
    this.calculatePagination();
  }
}
exportToExcel(): void {
  if (!this.rows || this.rows.length === 0) {
    return;
  }

  const workbook = new ExcelJS.Workbook();
  const worksheet = workbook.addWorksheet('Branch Daily Summary');

  // الهيدر
  const header: string[] = [
    'الفرع',
    'نقدية',
    'شبكة',
    'آجل',
    'إجمالي البيع',
    'الإجمالي الكلي',
    'الفرق',
    'قيمة العجز'
  ];

  // الأعمدة الديناميكية (أنواع العجز)
  this.shortageTypes.forEach(st => {
    header.push(st.name);
  });

  worksheet.addRow(header);

  // البيانات
  this.rows.forEach(row => {
    const rowData: any[] = [
      row.branchName,
      row.cashAmount,
      row.networkAmount,
      row.creditAmount,
      row.totalSales,
      row.grandTotal,
      row.difference,
      row.totalShortageAmount
    ];

    this.shortageTypes.forEach(st => {
      const found = row.shortages?.find(x => x.shortageTypeId === st.id);
      rowData.push(found ? found.amount : 0);
    });

    worksheet.addRow(rowData);
  });

  // تنسيق بسيط
  worksheet.columns.forEach(col => {
    col.width = 18;
  });

  workbook.xlsx.writeBuffer().then(buffer => {
    saveAs(new Blob([buffer]), 'BranchDailySummary.xlsx');
  });
}

exportToPdf(): void {
  if (!this.rows || this.rows.length === 0) return;

  const doc = new jsPDF('l', 'mm', 'a4');

  // تحميل الخط العربي
  (doc as any).addFont('assets/fonts/Amiri-Regular.ttf', 'Amiri', 'normal');
  doc.setFont('Amiri');
  doc.setFontSize(14);

  // عنوان التقرير
  doc.text('تقرير يوميات الفروع المجمع', 148, 15, { align: 'center' });

  // عرض الأعمدة (نفس اللي autoTable بيستخدمه)
  const colWidths = [
    35, // الفرع
    18, // نقدية
    18, // شبكة
    18, // آجل
    22, // إجمالي البيع
    22, // الإجمالي الكلي
    18, // الفرق
    22  // قيمة العجز
  ];

  // إضافة الأعمدة الديناميكية (أنواع العجز)
  this.shortageTypes.forEach(() => colWidths.push(20));

  // رسم الهيدر يدويًا بالعربي فوق الأعمدة
  doc.setFontSize(10);
  let x = 10;

  const headers = [
    'الفرع',
    'نقدية',
    'شبكة',
    'آجل',
    'إجمالي البيع',
    'الإجمالي الكلي',
    'الفرق',
    'قيمة العجز'
  ];

  headers.forEach((h, i) => {
    doc.text(h, x + colWidths[i] / 2, 25, { align: 'center' });
    x += colWidths[i];
  });

  // الأعمدة الديناميكية
  this.shortageTypes.forEach(st => {
    doc.text(st.name, x + 10, 25, { align: 'center' });
    x += 20;
  });

  // تجهيز البيانات
  const body = this.rows.map(row => {
    const rowData = [
      row.branchName,
      row.cashAmount?.toFixed(2),
      row.networkAmount?.toFixed(2),
      row.creditAmount?.toFixed(2),
      row.totalSales?.toFixed(2),
      row.grandTotal?.toFixed(2),
      row.difference?.toFixed(2),
      row.totalShortageAmount?.toFixed(2)
    ];

    this.shortageTypes.forEach(st => {
      const found = row.shortages?.find(x => x.shortageTypeId === st.id);
      rowData.push((found?.amount ?? 0).toFixed(2));
    });

    return rowData;
  });

  // تجهيز columnStyles بدون أخطاء TS
  const columnStyles: Record<number, any> = {};
  colWidths.forEach((w, i) => {
    columnStyles[i] = { cellWidth: w, halign: 'center' };
  });

  // رسم الجدول بدون هيدر داخلي
  autoTable(doc, {
    body,
    startY: 30,
    styles: {
      font: 'Amiri',
      fontSize: 9,
      halign: 'center',
      cellPadding: 2
    },
    columnStyles,
    margin: { top: 20, right: 10, left: 10 },
    showHead: 'never' // ← مهم جدًا
  });

  doc.save('BranchDailySummary.pdf');
}






}
