import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Console, error } from 'console';
import { response } from 'express';
import { Product } from './shared/Models/product';
import { Pagination } from './shared/Models/Pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'client';
 
  constructor() {
   
  }
  
}
