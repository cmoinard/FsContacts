module Types

type LoadableResult<'TLoading, 'TLoaded> =
| Loading of 'TLoading
| Loaded of 'TLoaded
| Error of exn