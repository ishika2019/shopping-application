import { Component, Input, Output,EventEmitter} from '@angular/core';


@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrl: './pager.component.scss'
})
export class PagerComponent {
 @Input() totalcount?:number;
 @Input() pageSize?:number;
 @Output() pageChanged=new EventEmitter<number>();

 onPagerChanged(event:any)
 {     console.log(event);
       this.pageChanged.emit(event.page);
 }
}
