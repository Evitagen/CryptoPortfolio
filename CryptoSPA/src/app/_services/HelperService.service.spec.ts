/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { HelperServiceService } from './HelperService.service';

describe('Service: HelperService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HelperServiceService]
    });
  });

  it('should ...', inject([HelperServiceService], (service: HelperServiceService) => {
    expect(service).toBeTruthy();
  }));
});
