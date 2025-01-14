import { Component, OnInit } from '@angular/core';
import { IconDirective } from '@coreui/icons-angular';
import {
  ContainerComponent,
  RowComponent,
  ColComponent,
  InputGroupComponent,
  InputGroupTextDirective,
  FormControlDirective,
  ButtonDirective,
  TableModule,
  UtilitiesModule,
} from '@coreui/angular';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';

interface User {
  username: string;
  email: string;
  password: string;
}

@Component({
  selector: 'app-user-list',
  templateUrl: './userList.component.html',
  styleUrls: ['./userList.component.scss'],
  imports: [
    TableModule,
    CommonModule,
    UtilitiesModule,
    ContainerComponent,
    HttpClientModule,
    RowComponent,
    ColComponent,
    InputGroupComponent,
    InputGroupTextDirective,
    IconDirective,
    FormControlDirective,
    ButtonDirective,
  ],
})
export class UserListComponent implements OnInit {
  users: any = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    const url = 'https://localhost:7121/api/Auth/GetAllUsers';
    this.http
      .get(url)
      .toPromise()
      .then((result) => {
        this.users = result;
      })
      .catch((err: any) => {
        alert(err);
      });
  }
}
