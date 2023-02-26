import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarouselHomeComponent } from './carousel-home.component';

describe('CarouselHomeComponent', () => {
  let component: CarouselHomeComponent;
  let fixture: ComponentFixture<CarouselHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarouselHomeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CarouselHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
