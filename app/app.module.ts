import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CoreModule } from "./core/core.module";
import { HomeModule } from './home/home.module';
import { errorInterceptor } from './core/interceptors/error.interceptor';
import { loadingInterceptor } from './core/interceptors/loading.interceptor';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
@NgModule({
    declarations: [
        AppComponent,
    ],
    providers: [
        provideClientHydration(),{
        provide: HTTP_INTERCEPTORS,
    useClass: errorInterceptor,
    multi: true
        },
        {
            provide: HTTP_INTERCEPTORS,
        useClass: loadingInterceptor,
        multi: true
            } 
    ],
    bootstrap: [AppComponent],
    imports: [
        BrowserModule,
        AppRoutingModule,
        NgbModule,
        BrowserAnimationsModule,
        HttpClientModule,
        CoreModule,
        HomeModule,
        NgxSpinnerModule,
        ProgressSpinnerModule
    
    ]
})
export class AppModule { }
