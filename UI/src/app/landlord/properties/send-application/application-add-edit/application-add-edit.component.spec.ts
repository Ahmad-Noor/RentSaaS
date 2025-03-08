import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationAddEditComponent } from './application-add-edit.component';

describe('ApplicationAddEditComponent', () => {
  let component: ApplicationAddEditComponent;
  let fixture: ComponentFixture<ApplicationAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApplicationAddEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ApplicationAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
