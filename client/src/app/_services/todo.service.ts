import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TranscodeEncoding } from 'buffer';
import { environment } from 'src/environments/environment';
import { Todo } from '../_models/todo';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
baseUrl = environment.apiUrl;
todos: Todo[] = [];
  constructor(private http: HttpClient) { }
  getTodos()
  {
    if(this.todos.length > 0) return this.todos;
    return this.http.get<Todo[]>(this.baseUrl + 'todoItems/all').
     subscribe(todos => {
       this.todos = todos;
       return todos;
     })
  }
  getTodo(id : number)
  {
    if(this.todos.length > 0) return this.todos;
    return this.http.get<Todo[]>(this.baseUrl + 'todoItems/' + id).
     subscribe(todos => {
       this.todos = todos;
       return todos;
     })
  }
}

