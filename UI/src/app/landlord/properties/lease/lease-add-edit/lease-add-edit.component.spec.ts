import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaseAddEditComponent } from './lease-add-edit.component';

describe('LeaseAddEditComponent', () => {
  let component: LeaseAddEditComponent;
  let fixture: ComponentFixture<LeaseAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LeaseAddEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeaseAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
