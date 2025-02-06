import { Routes } from '@angular/router';

export const routes: Routes = [
    { path: '', loadComponent() { return import('./pages/inicio/inicio.component').then(m => m.InicioComponent); }},
    { path: 'detalle/:id', loadComponent() { return import('./pages/detalle/detalle.component').then(m => m.DetalleComponent); }},
    { path: '**', loadComponent() { return import('./pages/not-found/not-found.component').then(m => m.NotFoundComponent); }},
];