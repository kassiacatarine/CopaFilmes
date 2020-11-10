import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StandingDetailsComponent } from './standing-details.component';

describe('StandingDetailsComponent', () => {
  let component: StandingDetailsComponent;
  let fixture: ComponentFixture<StandingDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StandingDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StandingDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
