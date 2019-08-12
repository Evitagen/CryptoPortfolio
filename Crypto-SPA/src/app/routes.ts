import {Routes} from '@angular/router';

import { CoinlistComponent } from './coinlist/coinlist.component';
import { RegisterComponent } from './register/register.component';

export const appRoutes: Routes = [
    {path: 'coin', component: CoinlistComponent},
    {path: 'register', component: RegisterComponent},
    {path: '**', redirectTo: 'coin', pathMatch: 'full'},
];
