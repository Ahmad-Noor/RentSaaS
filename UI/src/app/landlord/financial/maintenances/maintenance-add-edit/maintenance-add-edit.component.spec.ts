import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MaintenanceAddEditComponent } from './maintenance-add-edit.component';

describe('MaintenanceAddEditComponent', () => {
  let component: MaintenanceAddEditComponent;
  let fixture: ComponentFixture<MaintenanceAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaintenanceAddEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MaintenanceAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
