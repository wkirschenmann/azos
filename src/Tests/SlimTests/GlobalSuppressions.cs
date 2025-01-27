﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System;
using System.Diagnostics.CodeAnalysis;

[assembly: CLSCompliant(true)]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "Fix not compatible with multi-targeting net45", Scope = "namespaceanddescendants", Target = "~N:SlimTests")]
[assembly: SuppressMessage("Style", "IDE0090:Use 'new(...)'", Justification = "Fix not compatible with multi-targeting net45", Scope = "namespaceanddescendants", Target = "~N:SlimTests")]
