import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactComponent } from './contact/contact.component';
import { DisclaimerComponent } from './disclaimer/disclaimer.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { HomeComponent } from './home/home.component';
import { AddTodoComponent } from './todos/add-todo/add-todo.component';
import { AllComponent } from './todos/all/all.component';
import { DetailsComponent } from './todos/details/details.component';
import { OverdueComponent } from './todos/overdue/overdue.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'contact', component: ContactComponent},
  {path: 'disclaimer', component: DisclaimerComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'todos/all', component: AllComponent},
      {path: 'todos/all/:{id}', component: DetailsComponent},
      {path: 'add-todo', component: AddTodoComponent},
      {path: 'todo/overdue', component: OverdueComponent}
    ]

  },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  {path: '*', component: NotFoundComponent, pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
