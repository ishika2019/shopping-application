import { Component } from '@angular/core';
import { environment } from '../../../environment/environment';
import { HttpClient } from '@angular/common/http';
import { tick } from '@angular/core/testing';
import { error } from 'console';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.scss'
})
export class TestErrorComponent {

  baseUrl=environment.apiUrl;
  validationError: string[]=[];
  
  constructor(private http:HttpClient) {
   
  }

  get404Error()
  {
    this.http.get(this.baseUrl+'product/42').subscribe({
      next: response=>console.log(response),
      error:error=>console.log(error)
    });
  }
  get500Error()
  {
    this.http.get(this.baseUrl+'buggy/servererror').subscribe({
      next: response=>console.log(response),
      error:error=>console.log(error)
    });
  }
  get400Error()
  {
    this.http.get(this.baseUrl+'buggy/badrequest').subscribe({
      next: response=>console.log(response),
      error:error=>console.log(error)
    });
  }
  get400ValidationError()
  {
    this.http.get(this.baseUrl+'product/fortytwo').subscribe({
      next: response=>console.log(response),
      error:error=>
      {
        console.log(error),
        this.validationError=error.errors;
      }
    });
  }

}
