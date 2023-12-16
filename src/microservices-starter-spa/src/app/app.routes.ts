import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { MainPageComponent } from './components/main-page/main-page.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { loggedInGuard } from './guards/logged-in.guard';

export const routes: Routes = [
  {
    path: '',
    pathMatch: "full",
    component: MainPageComponent
  },
  {
    path: "login",
    pathMatch: "full",
    component: LoginComponent
  },
  {
    path: "register",
    pathMatch: "full",
    component: RegisterComponent
  },
  {
    path: "dashboard",
    pathMatch: "full",
    component: DashboardComponent,
    canActivate: [loggedInGuard]
  }
];
