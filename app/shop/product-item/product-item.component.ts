import { Component, Input, input } from '@angular/core';
import { Product } from '../../shared/Models/product';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.scss'
})
export class ProductItemComponent {
   @Input() product?:Product;


   
   constructor(private basketService:BasketService) {
    
   }
   addItemToBasket()
   {
    this.product && this.basketService.addItemToBasket(this.product);
   }
}
