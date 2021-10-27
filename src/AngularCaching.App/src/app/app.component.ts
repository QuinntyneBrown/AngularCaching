import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToDo, ToDoService } from '@api';
import { AppStateService } from '@core';
import { map, tap } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  public form: FormGroup = new FormGroup({
    toDoId: new FormControl(null, []),
    description: new FormControl(null, [])
  })

  public vm$ = this._appStateService.getEntities$()
  .pipe(
    map(toDos => ({ toDos }))
  )

  constructor(
    private readonly _appStateService: AppStateService,
    private readonly _toDoService: ToDoService
  ) { }

  public save(toDo: ToDo) {
    this._toDoService.create({
      toDo
    })
    .pipe(
      tap(_ => {
        this.form.reset();
        this._appStateService.refreshEntities();
      })
    )
    .subscribe();
  }
}
