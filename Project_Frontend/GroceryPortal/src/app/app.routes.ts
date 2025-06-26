import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  {
    path: 'login',
    loadComponent: () => import('./auth/login/login').then(m => m.Login)
  },
  {
    path: 'register',
    loadComponent: () => import('./auth/register/register').then(m => m.Register)
  },
  {
    path: 'admin/home',
    loadComponent: () => import('./roles/admin/home/home').then(m => m.Home)
  },
  {
    path: 'customer/home',
    loadComponent: () => import('./roles/customer/home/home').then(m => m.Home)
  },
  {
    path: 'agent/home',
    loadComponent: () => import('./roles/agent/home/home').then(m => m.Home)
  },
  { path: '**', redirectTo: 'login' }
];
