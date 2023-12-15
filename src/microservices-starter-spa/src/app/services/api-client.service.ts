import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiClientService {
  private httpClient = inject(HttpClient);
  private apiBaseUrl = environment.apiBaseUrl;

  get<T>(url: string) {
    return this.httpClient.get<T>(`${this.apiBaseUrl}/${url}`);
  }

  post<T>(url: string, body: any) {
    return this.httpClient.post<T>(`${this.apiBaseUrl}/${url}`, body);
  }

  put<T>(url: string, body: any) {

  }

  patch<T>(url: string, body: any) {

  }

  delete<T>(url: string) {

  }
}
