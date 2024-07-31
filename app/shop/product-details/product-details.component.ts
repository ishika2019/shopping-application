import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../shared/Models/product';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit{

  product?:Product;
  constructor(private shopservice:ShopService,private route:ActivatedRoute,private bcService:BreadcrumbService) {
       this.bcService.set('@productdetails',' ');
  }
  ngOnInit(): void {
    var id=this.route.snapshot.paramMap.get('id');
    if(id)this.shopservice.getProduct(+id).subscribe(
      {
        next: response=>
        {this.product=response,
          this.bcService.set('@productdetails',this.product.name)

        }
      }
    );
  }

}
