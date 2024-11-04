---
optionsClassName: PauseAfterEachItemOptions
optionsClassFullName: MigrationTools.Enrichers.PauseAfterEachItemOptions
configurationSamples:
- name: defaults
  order: 2
  description: 
  code: There are no defaults! Check the sample for options!
  sampleFor: MigrationTools.Enrichers.PauseAfterEachItemOptions
- name: sample
  order: 1
  description: 
  code: There is no sample, but you can check the classic below for a general feel.
  sampleFor: MigrationTools.Enrichers.PauseAfterEachItemOptions
- name: classic
  order: 3
  description: 
  code: >-
    {
      "$type": "PauseAfterEachItemOptions",
      "Enabled": false,
      "OptionFor": "PauseAfterEachItem",
      "RefName": null
    }
  sampleFor: MigrationTools.Enrichers.PauseAfterEachItemOptions
description: missing XML code comments
className: PauseAfterEachItem
typeName: ProcessorEnrichers
architecture: 
options:
- parameterName: Enabled
  type: Boolean
  description: If enabled this will run this migrator
  defaultValue: true
- parameterName: OptionFor
  type: String
  description: missing XML code comments
  defaultValue: missing XML code comments
- parameterName: RefName
  type: String
  description: For internal use
  defaultValue: missing XML code comments
status: missing XML code comments
processingTarget: missing XML code comments
classFile: /src/MigrationTools/Processors/Enrichers/PauseAfterEachItem.cs
optionsClassFile: /src/MigrationTools/Processors/Enrichers/PauseAfterEachItemOptions.cs

redirectFrom:
- /Reference/ProcessorEnrichers/PauseAfterEachItemOptions/
layout: reference
toc: true
permalink: /Reference/ProcessorEnrichers/PauseAfterEachItem/
title: PauseAfterEachItem
categories:
- ProcessorEnrichers
- 
topics:
- topic: notes
  path: /docs/Reference/ProcessorEnrichers/PauseAfterEachItem-notes.md
  exists: false
  markdown: ''
- topic: introduction
  path: /docs/Reference/ProcessorEnrichers/PauseAfterEachItem-introduction.md
  exists: false
  markdown: ''

---