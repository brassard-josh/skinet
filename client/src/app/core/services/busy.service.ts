import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequestCount = 0;

  constructor(private sprinnerService: NgxSpinnerService) { }

  busy() {
    this.busyRequestCount++;
    this.sprinnerService.show(undefined, {
      type: 'timer',
      size: 'medium',
      bdColor: 'rgba(255, 255, 255, 0.7)',
      color: '#333',
      zIndex: 100000000
    });
  }

  idle() {
    this.busyRequestCount--;
    if (this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
      this.sprinnerService.hide();
    }
  }
}
