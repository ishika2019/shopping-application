import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, Type } from '@angular/core';
import { Pagination } from '../shared/Models/Pagination';
import { Product } from '../shared/Models/product';
import { Brand } from '../shared/Models/brand';
import { Types } from '../shared/Models/type';
import { shopParams } from '../shared/Models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  constructor(private http:HttpClient) { }

   getProducts(shopparams:shopParams)
   {
    let params=new HttpParams();
    if(shopparams.brandId) params=params.append('brandId',shopparams.brandId);
    if(shopparams.typeId) params=params.append('typeId',shopparams.typeId);
     params=params.append('sort',shopparams.sort);
     params=params.append('pageIndex',shopparams.pageNumber);
     params=params.append('pageSize',shopparams.pageSize);
     if(shopparams.search) params=params.append('search',shopparams.search);

    return  this.http.get<Pagination<Product[]>>('https://localhost:7272/api/Product',{params});
   }
   getBrands()
   {
    return  this.http.get<Brand[]>('https://localhost:7272/api/Product/brand');
   }
   getTypes()
   {
    return  this.http.get<Types[]>('https://localhost:7272/api/Product/type');
   }
   getProduct(id:number)
   {
        return this.http.get<Product>('https://localhost:7272/api/Product/'+id);
   }
}
