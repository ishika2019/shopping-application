import cuid from "cuid";

export interface BasketItem
{
    itemId:number;
    productName:string;
    price:number;
    quantity:number;
    pictureUrl:string;
    brand:string;
    type:string;
    customerBasketId:string;
}

export interface Basket{
    id:string;
    items:BasketItem[];
}

export class Basket implements Basket{
    id= cuid();
    items:BasketItem[]=[];
}