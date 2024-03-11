import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class MailService {
  private apiUrl = 'https://reenbitblob20240311032020.azurewebsites.net/api/Blob';

  constructor(private http: HttpClient) { }

    createItem(file: FormData): Observable<FormData> {
    return this.http.post<FormData>(this.apiUrl, file);
  }
}
