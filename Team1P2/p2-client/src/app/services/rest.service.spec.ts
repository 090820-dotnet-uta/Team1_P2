import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { User } from '../models/user.model';

import { RestService } from './rest.service';

describe('RestService', () => {
  let service: RestService;

  let mockPost:User;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [RestService]
    });
    service = TestBed.inject(RestService);
    let u = new User();
    u.userId = 1;
    u.username = "my username";
    u.password = "this is my password";
    u.name = "this is my name";
    u.screenName = "this is my screenName";
    mockPost = u;
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

});
