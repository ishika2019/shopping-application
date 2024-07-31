import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, delay, finalize, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { EOF } from 'dns';
import { ToastrService } from 'ngx-toastr';
import { BusyService } from '../services/busy.service';

@Injectable()
export class loadingInterceptor implements HttpInterceptor {
       
        constructor(private busyservice:BusyService) {
         
        }
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
   
   this.busyservice.busy();
  return next.handle(request).pipe(
    delay(1000),
    finalize(()=>this.busyservice.idle()) 
  );
  }
}