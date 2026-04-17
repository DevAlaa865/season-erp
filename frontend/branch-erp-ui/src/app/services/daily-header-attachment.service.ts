import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class DailyHeaderAttachmentService {
  private baseUrl = 'https://localhost:7025/api/DailyHeaderAttachments'; // عدّل البورت حسب الـ API

  constructor(private http: HttpClient) {}

  uploadHeader(file: File): Observable<string> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http
      .post<{ success: boolean; path: string }>(`${this.baseUrl}/upload`, formData)
      .pipe(map(res => res.path));
  }
}
