import {
  PaymentForm,
  init_payment_form
} from "./chunk-R2M3ZNQL.js";
import {
  TestBed,
  init_testing
} from "./chunk-5ORQCVZD.js";
import {
  __async,
  __commonJS
} from "./chunk-TTULUY32.js";

// src/app/payment-form/payment-form.spec.ts
var require_payment_form_spec = __commonJS({
  "src/app/payment-form/payment-form.spec.ts"(exports) {
    init_testing();
    init_payment_form();
    describe("PaymentFormComponent", () => {
      let component;
      let fixture;
      beforeEach(() => __async(null, null, function* () {
        yield TestBed.configureTestingModule({
          imports: [PaymentForm]
        }).compileComponents();
        fixture = TestBed.createComponent(PaymentForm);
        component = fixture.componentInstance;
        window.Razorpay = function() {
          return { open: jasmine.createSpy("open") };
        };
        fixture.detectChanges();
      }));
      it("should create the component", () => {
        expect(component).toBeTruthy();
      });
      it("should mark form invalid if fields are empty", () => {
        component.paymentForm.setValue({
          amount: "",
          customerName: "",
          email: "",
          contactNumber: ""
        });
        expect(component.paymentForm.invalid).toBeTrue();
      });
      it("should mark form valid with correct input", () => {
        component.paymentForm.setValue({
          amount: 100,
          customerName: "Test",
          email: "test@example.com",
          contactNumber: "9876543210"
        });
        expect(component.paymentForm.valid).toBeTrue();
      });
      it("should show email as invalid if bad email given", () => {
        component.paymentForm.setValue({
          amount: 100,
          customerName: "User",
          email: "bademail",
          contactNumber: "9876543210"
        });
        expect(component.paymentForm.get("email")?.valid).toBeFalse();
      });
      it("should call Razorpay.open() on valid form submit", () => {
        const openSpy = jasmine.createSpy("open");
        window.Razorpay = function() {
          return { open: openSpy };
        };
        component.paymentForm.setValue({
          amount: 100,
          customerName: "Tester",
          email: "t@example.com",
          contactNumber: "9999999999"
        });
        component.onSubmit();
        expect(openSpy).toHaveBeenCalled();
      });
    });
  }
});
export default require_payment_form_spec();
//# sourceMappingURL=spec-app-payment-form-payment-form.spec.js.map
