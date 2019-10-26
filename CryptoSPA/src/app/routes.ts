import {Routes} from '@angular/router';

import { CoinlistComponent } from './coinlist/coinlist.component';
import { RegisterComponent } from './register/register.component';
import { CoinPortfolioComponent } from './members/coinPortfolio/coinPortfolio.component';
import { AuthGuard } from './_guards/auth.guard';
import { SelectPortfolioComponent } from './members/SelectPortfolio/SelectPortfolio.component';

export const appRoutes: Routes = [
    {path: 'coin', component: CoinlistComponent},
    {path: 'register', component: RegisterComponent},
    {path: 'coinportfolio/:id', component: CoinPortfolioComponent, canActivate: [AuthGuard]},
    {path: 'selectportfolio', component: SelectPortfolioComponent, canActivate: [AuthGuard]},
    {path: '**', redirectTo: 'coin', pathMatch: 'full'},
];
