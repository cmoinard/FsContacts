module ValidationState

type ValidationState =
| Validated
| Errors of string

type ValidatedValue<'T> = {
    value: 'T
    state: ValidationState
}