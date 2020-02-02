import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UrlDTO } from './Models/UrlDTO';

@Injectable({
  providedIn: 'root'
})
export class UrlService{

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }





  public searchUrl(url: UrlDTO):Observable<UrlDTO> {
    let hdr = {
      'Content-Type': 'application/json'
    };
    let options = { headers: hdr };
    return this.http.post<UrlDTO>(this.baseUrl + 'url/search', url, options);
  }

  public CreateUrl(url: UrlDTO):Observable<UrlDTO> {
    let hdr = {
      'Content-Type': 'application/json'
    };
    let options = { headers: hdr };
    return this.http.post<UrlDTO>(this.baseUrl + 'url/Create', url, options);
  }
}
