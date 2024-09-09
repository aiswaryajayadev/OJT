import { Routes } from '@angular/router';
import { FormComponentComponent } from './form-component/form-component.component';
import { VisitorListComponent } from './visitor-list/visitor-list.component';

export const routes: Routes = [

    {
        path:'',component:FormComponentComponent
    },
    {
        path:'visitorList',component:VisitorListComponent
    }
];
