import { Component, ElementRef, OnInit, ViewChild, viewChild } from '@angular/core';
import { ShopService } from './shop.service';
import { Product } from '../shared/Models/product';
import { error } from 'console';
import { response } from 'express';
import { Brand } from '../shared/Models/brand';
import { Types } from '../shared/Models/type';
import { shopParams } from '../shared/Models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {

  products: Product[]=[];
  brands: Brand[]=[];
  types: Types[]=[];
  shopparams=new shopParams();
  totalcount=0;
  sortOptions=[{name:'Alphabetical',value:'name'},
  {name:'Price: Low to High',value:'priceAsc'},
  {name:'Price: High to Low',value:'priceDesc'}
  
]
  constructor(private shopService:ShopService) {
    
    
  }
  ngOnInit(): void {
    
    this.getProduct();
    this.getBrand();
    this.getType();
  }
  @ViewChild('search') searchTerm?:ElementRef;
  getProduct()
  {
    this.shopService.getProducts(this.shopparams).subscribe(
      {
        next:(response)=>{this.products=response.data;
             this.shopparams.pageNumber=response.pageIndex;
             this.shopparams.pageSize=response.pageSize;
             this.totalcount=response.count;
            },
        error:(error)=>console.log(error)

      }
    );
  }
  getBrand()
  {
    this.shopService.getBrands().subscribe(
      {
        next:(response)=>this.brands=[{id:0,name:'All'},...response],
        error:(error)=>console.log(error)

      }
    );
  }
  getType()
  {
    this.shopService.getTypes().subscribe(
      {
        next:(response)=>this.types=[{id:0,name:'All'},...response],
        error:(error)=>console.log(error)

      }
    );
  }
  onBrandSelected(brandId:number)
  {
    this.shopparams.brandId=brandId;
   this.shopparams.pageNumber=1;
    this.getProduct();
  }
  onTypeSelected(typeId:number)
  {
    this.shopparams.typeId=typeId;
    this.shopparams.pageNumber=1;
    this.getProduct();
  }

  onSortSelected(event:any)
{
  this.shopparams.sort=event.target.value;
  this.getProduct();
}

onPageChanged(event:any)
{
  console.log(event); 
  if(this.shopparams.pageNumber!=event)
  this.shopparams.pageNumber=event;
  console.log(this.shopparams.pageNumber);
    this.getProduct();
}
onSearch()
{
  this.shopparams.search=this.searchTerm?.nativeElement.value;
  this.getProduct();
}
onReset()
{
  if(this.searchTerm)
  this.searchTerm.nativeElement.value="";
this.shopparams=new shopParams();
this.getProduct();
   
  
}


}
