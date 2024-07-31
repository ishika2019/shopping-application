import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  badRequestCount=0;
  constructor(private spinnerservice:NgxSpinnerService) { }

  busy()
  {
       this.badRequestCount++;
       this.spinnerservice.show(
          undefined,{
            type:'timer',
            bdColor:'rgba(255,255,255,0.7)',
            color:'#333333'
          }
       );

  }

  idle(){
    this.badRequestCount--;
    if(this.badRequestCount<=0)
    {
      this.badRequestCount=0;
      this.spinnerservice.hide();
    }


  }
}
