import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvertisingAddEditComponent } from './advertising-add-edit.component';

describe('AdvertisingAddEditComponent', () => {
  let component: AdvertisingAddEditComponent;
  let fixture: ComponentFixture<AdvertisingAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdvertisingAddEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdvertisingAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
