module ValidationState

type ValidationState =
| Validated
| Errors of string list

type ValidatedValue<'T> = {
    value: 'T
    state: ValidationState
}