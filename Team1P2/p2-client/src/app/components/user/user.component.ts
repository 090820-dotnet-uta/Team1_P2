import { Component, OnInit } from '@angular/core';
import { User } from "../../models/user.model";
import { UserRepository } from '../../models/user.repository';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  constructor(private repo: UserRepository){}

  ngOnInit(): void {
  }

  get user(): User {
    return this.repo.getUser();
  }

  get users(): User[] {
    return this.repo
      .getUsers();
  }

}
