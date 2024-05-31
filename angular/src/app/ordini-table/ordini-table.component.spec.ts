import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrdiniTableComponent } from './ordini-table.component';

describe('OrdiniTableComponent', () => {
  let component: OrdiniTableComponent;
  let fixture: ComponentFixture<OrdiniTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrdiniTableComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OrdiniTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
