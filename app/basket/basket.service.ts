import { Injectable } from '@angular/core';
import { Basket, BasketItem } from '../shared/Models/basket';
import { Product } from '../shared/Models/product';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environment/environment';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl=environment.apiUrl;
  private basketSource=new BehaviorSubject<Basket|null >(null);
  basketSource$ =this.basketSource.asObservable();

  
  constructor(private http :HttpClient) {
   
    
  }

  getBasket(id:string)
  {
    return this.http.get<Basket>(this.baseUrl+'basket?id='+id).subscribe(
      {
        next: basket=>this.basketSource.next(basket),
      }
    );

  }
  setBasket(basket:Basket)
  {
    return this.http.post<Basket>(this.baseUrl+'basket',basket).subscribe(
      {
        next: basket=>this.basketSource.next(basket),
      }
    );
  }
  getCurrentBasket()
  {
    return this.basketSource.value;
  }

  addItemToBasket(product:Product,quantity=1)
  {
    const itemToAdd=this.mapProductItemToBasketItem(product);
    const basket=this.getCurrentBasket()?? this.createBasket();
    itemToAdd.customerBasketId=basket.id;
    basket.items=this.addOrUpdate(basket.items,itemToAdd,quantity);
    console.log(basket.items);
    this.setBasket(basket);
  }
 addOrUpdate(items: BasketItem[], itemToAdd: BasketItem, quantity: number): BasketItem[] {
    const item=items.find(x=>x.itemId===itemToAdd.itemId);
    console.log("item"+item);
    if(item){
  
      itemToAdd.quantity += quantity;
    items.forEach(element => {
      if(element.itemId==item.itemId)
        element.quantity =element.quantity+1;
      });
      
    }
    else{
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    return items;
  }
  

  createBasket(): Basket  {
   const basket=new Basket();
   basket.items=[];
   localStorage.setItem('basket_id',basket.id);
   return basket;
  }

  private mapProductItemToBasketItem(product :Product):BasketItem
   {
    console.log(product);
    console.log(product.productbrand);
    return {
        itemId:product.id,
        productName:product.name,
        price:product.price,
        pictureUrl:product.pictureUrl,
        brand:product.productbrand,
        type:product.producttype,
        quantity:0,
        customerBasketId:""
    }
   }
}
