import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { EOF } from 'dns';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class errorInterceptor implements HttpInterceptor {
  constructor(private route:Router,private toaster:ToastrService) {
   
  }


  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
   
   
  return next.handle(request).pipe(
    catchError((error: HttpErrorResponse)=>{
      if(error)
      {
             if(error.status==400)
             {
              if(error.error.errors)
              {
                throw error.error;
              }
              else
              {
                this.toaster.error(error.error.message,error.status.toString());
              }
             }
             if(error.status==404)
             {
               this.toaster.error(error.error.message,error.status.toString());
             }
             if(error.status===400)
             {
              this.route.navigateByUrl('/not-found');
             }
             if(error.status===500)
             {
              const navigationExtras:NavigationExtras={state:{error:error.error}};
              this.route.navigateByUrl('/server-error',navigationExtras);
             };
            
    }
    return throwError(()=>new Error(error.message));
      }
    )
  )
}
}
