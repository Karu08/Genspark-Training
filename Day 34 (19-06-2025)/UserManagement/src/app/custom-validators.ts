import {AbstractControl, ValidationErrors, ValidatorFn, FormGroup } from '@angular/forms';

export class CustomValidators {
    static bannedUsernameValidator(bannedWords: string[]): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
            const value = control.value?.toLowerCase();
            if(bannedWords.some(word=>value?.includes(word))) {
                return {banned: true};
            }
            return null;
        };
    }

    static passwordStrengthValidator(): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
            const value = control.value;
            if(!value) return null;
            const hasNumber = /\d/.test(value);
            const hasSymbol = /[!@#$%^&*]/.test(value);
            const isLongEnough = value.length >= 8;
            return hasNumber && hasSymbol && isLongEnough ? null : {weakPassword: true};
        };
    }

    static confirmPasswordValidator(passwordKey: string, confirmPasswordKey: string): ValidatorFn {
        return (group: AbstractControl): ValidationErrors | null => {
            const form = group as FormGroup;
            const password = form.get(passwordKey)?.value;
            const confirmPassword = form.get(confirmPasswordKey)?.value;
            return password===confirmPassword ? null : {mismatch: true};
        };
    }
}