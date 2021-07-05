import { Component, Input, OnInit } from '@angular/core';
import { Todo } from 'src/app/_models/todo';

@Component({
  selector: 'app-todo-card',
  templateUrl: './todo-card.component.html',
  styleUrls: ['./todo-card.component.css']
})
export class TodoCardComponent implements OnInit {
  @Input()
  todo!: Todo;
  constructor() { }

  ngOnInit(): void {
  }

}
