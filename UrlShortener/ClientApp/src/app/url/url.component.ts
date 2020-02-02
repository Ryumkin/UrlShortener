import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService } from '../url.service';
import { UrlDTO } from '../Models/UrlDTO';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, } from 'rxjs';
import { filter, switchMap } from 'rxjs/operators';
import { Route } from '@angular/compiler/src/core';

@Component({
  selector: 'app-url',
  templateUrl: './url.component.html'
})
export class UrlComponent {

  public url: string;
  public shortUrl: string;

  constructor(private urlService: UrlService, 
    private activatedRoute: ActivatedRoute, 
    private router: Router,
    @Inject('BASE_URL') private baseUrl: string) {
    this.activatedRoute.url
      .pipe(filter(x => !!(x)),
        switchMap(x => this.urlService.searchUrl(new UrlDTO(x.pop().path))),
        filter(x => !!(x&&x.url))
      )
      .subscribe(x => {
        window.location.href = x.url
      })

  }

  public createShortUrl(): void{
    this.shortUrl = null;
    this.urlService.CreateUrl(new UrlDTO(this.url))
    .subscribe( x => 
        this.shortUrl = this.baseUrl+x.url
    );
  }
}

