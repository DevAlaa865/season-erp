import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { BranchSalesDailyService } from '../../../services/branch-sales-daily.service';
import { jsPDF } from 'jspdf';
import { MasterDataService } from '../../../services/master-data.service';
import { ActivatedRoute, Router } from '@angular/router';
interface ShortageDetail {
  id: number;
  shortageTypeId: number;
  shortageTypeName: string;
  amount: number;
  attachmentPath: string | null;
  employeeId: number | null;
  employeeName: string | null;
}

interface BranchDailySalesReport {
  salesDate: string;
  branchName: string;
  supervisorName: string;

  cashAmount: number;
  networkAmount: number;
  creditAmount: number;
  totalSales: number;
  grandTotal: number;
  totalInvoicesCount: number;
  totalQuantities: number;
  difference: number;
  differenceLabel: string;

  shortageDetails: ShortageDetail[];
  attachmentPath: string | null;
  supervisorNotes: string | null;
}

@Component({
  selector: 'app-daily-sales-inquiry',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './daily-sales-inquiry.component.html'
})
export class DailySalesInquiryComponent implements OnInit {
 
  userInfo: any;
  isBranchUser = false;
  branches: any[] = [];

  daily: any;
  showEmployeeColumn = false;
  form!: FormGroup;

  branchName = '';
  branchId: number | null = null;

  isLoading = false;
  errorMessage = '';
  report: BranchDailySalesReport | null = null;
  
  fileBaseUrl = 'https://localhost:7025/'
  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private branchSalesDailyService: BranchSalesDailyService,
      private master: MasterDataService,
      private router:Router,
   private route: ActivatedRoute   // ← هنا
  ) {}
  apiBaseUrl = 'https://localhost:7025';
   getImageUrl(path: string | null | undefined): string {
    if (!path) return '';
    return `${this.apiBaseUrl}/${path}`;
  }
ngOnInit(): void {
  this.form = this.fb.group({
    salesDate: [null, Validators.required],
    branchId: [null]
  });

  this.userInfo = this.auth.getUserInfo();

  // لو المستخدم فرع
  if (this.userInfo && this.userInfo.branchId) {
    this.isBranchUser = true;
    this.branchId = this.userInfo.branchId;
    this.branchName = decodeURIComponent(escape(String(this.userInfo.branchName || '')));
    this.form.patchValue({ branchId: this.branchId });
  } else {
    // مستخدم مركزي
    this.isBranchUser = false;
    this.loadBranches();
  }

  // قراءة البرامترات القادمة من تقرير السمرى
  this.route.queryParams.subscribe((params: any) => {
    const branchIdFromQuery = params['branchId'];
    const dateFromQuery = params['salesDate'];

    if (branchIdFromQuery && dateFromQuery) {
      // لو المستخدم مركزي → نملأ الفرع
      if (!this.isBranchUser) {
        this.form.patchValue({
          branchId: Number(branchIdFromQuery)
        });
      }

      // نملأ التاريخ
      this.form.patchValue({
        salesDate: dateFromQuery
      });

      // نعمل استعلام أوتوماتيك
      setTimeout(() => {
        this.search();
      }, 200);

      return;
    }

    // لو مش جاي من السمرى → الوضع الطبيعي
    const today = new Date().toISOString().split('T')[0];
    this.form.patchValue({ salesDate: today });
  });
}

loadBranches() {
  this.master.getBranches().subscribe({
    next: res => {
      this.branches = res.data || [];
    },
    error: err => {
      console.error(err);
      this.errorMessage = 'حدث خطأ أثناء تحميل قائمة الفروع.';
    }
  });
}


  search(): void {
    this.errorMessage = '';
    this.report = null;

    if (this.form.invalid) {
      this.errorMessage = 'من فضلك اختر التاريخ أولاً.';
      return;
    }

   const date: string = this.form.value.salesDate;

// 🟦 تحديد الفرع حسب نوع المستخدم
let branchIdToUse: number | null = null;

if (this.isBranchUser) {
  branchIdToUse = this.branchId;
} else {
  branchIdToUse = this.form.value.branchId;
}

if (!branchIdToUse) {
  this.errorMessage = 'من فضلك اختر الفرع أولاً.';
  return;
}

    this.isLoading = true;

    this.branchSalesDailyService
      .getByBranchAndDate(branchIdToUse, date)
      .subscribe({
        next: (res: any) => {
          this.isLoading = false;
     
          if (!res || !res.data || res.data.length === 0) {
            this.errorMessage = 'لا توجد يومية لهذا التاريخ.';
            return;
          }

          const item = res.data[0];

          this.report = {
            salesDate: item.salesDate.split('T')[0],
            branchName: item.branchName,
            supervisorName: item.supervisorName,

            cashAmount: item.cashAmount,
            networkAmount: item.networkAmount,
            creditAmount: item.creditAmount,
            totalSales: item.totalSales,
            grandTotal: item.grandTotal,
            totalInvoicesCount: item.totalInvoicesCount,
            totalQuantities: item.totalQuantities,
             attachmentPath: item.attachmentPath ,
            difference: item.difference,
            differenceLabel: item.difference === 0 ? 'متوازن' : (item.difference > 0 ? 'زيادة' : 'عجز'),

            shortageDetails: item.shortageDetails || [],

            supervisorNotes: item.supervisorNotes || null
          };
          this.showEmployeeColumn = this.report.shortageDetails.some(x => x.employeeName);
        },
        error: (err) => {
          this.isLoading = false;
          this.errorMessage = 'حدث خطأ أثناء جلب التقرير';
          console.error(err);
        }
      });
  }
openImageAsPdf(path: string | null | undefined) {
  if (!path) return;

  const imageUrl = this.fileBaseUrl + path;

  fetch(imageUrl)
    .then(res => res.blob())
    .then(blob => {
      const reader = new FileReader();

      reader.onload = () => {
        const imgData = reader.result as string;

        const pdf = new jsPDF({
          orientation: 'portrait',
          unit: 'pt',
          format: 'a4'
        });

        // مقاسات الصفحة
        const pageWidth = pdf.internal.pageSize.getWidth();
        const pageHeight = pdf.internal.pageSize.getHeight();

        // إضافة الصورة بحيث تملأ الصفحة
        pdf.addImage(imgData, 'JPEG', 0, 0, pageWidth, pageHeight);

        // فتح PDF في تبويب جديد
        pdf.output('dataurlnewwindow');
      };

      reader.readAsDataURL(blob);
    });
}

 goBackToDashboard() {
    this.router.navigate(['/reports/branch-daily-summary']);
  }

}
