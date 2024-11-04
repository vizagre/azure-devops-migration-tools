---
optionsClassName: TfsExportProfilePictureFromADProcessorOptions
optionsClassFullName: MigrationTools.Processors.TfsExportProfilePictureFromADProcessorOptions
configurationSamples:
- name: defaults
  order: 2
  description: 
  code: There are no defaults! Check the sample for options!
  sampleFor: MigrationTools.Processors.TfsExportProfilePictureFromADProcessorOptions
- name: sample
  order: 1
  description: 
  code: There is no sample, but you can check the classic below for a general feel.
  sampleFor: MigrationTools.Processors.TfsExportProfilePictureFromADProcessorOptions
- name: classic
  order: 3
  description: 
  code: >-
    {
      "$type": "TfsExportProfilePictureFromADProcessorOptions",
      "Enabled": false,
      "Domain": null,
      "Username": null,
      "Password": null,
      "PictureEmpIDFormat": null,
      "SourceName": null,
      "TargetName": null
    }
  sampleFor: MigrationTools.Processors.TfsExportProfilePictureFromADProcessorOptions
description: Downloads corporate images and updates TFS/Azure DevOps profiles
className: TfsExportProfilePictureFromADProcessor
typeName: Processors
architecture: 
options:
- parameterName: Domain
  type: String
  description: The source domain where the pictures should be exported.
  defaultValue: String.Empty
- parameterName: Enabled
  type: Boolean
  description: If set to `true` then the processor will run. Set to `false` and the processor will not run.
  defaultValue: missing XML code comments
- parameterName: Password
  type: String
  description: The password of the user that is used to export the pictures.
  defaultValue: String.Empty
- parameterName: PictureEmpIDFormat
  type: String
  description: 'TODO: You wpuld need to customise this for your system. Clone repo and run in Debug'
  defaultValue: String.Empty
- parameterName: SourceName
  type: String
  description: missing XML code comments
  defaultValue: missing XML code comments
- parameterName: TargetName
  type: String
  description: missing XML code comments
  defaultValue: missing XML code comments
- parameterName: Username
  type: String
  description: The user name of the user that is used to export the pictures.
  defaultValue: String.Empty
status: alpha
processingTarget: Profiles
classFile: /src/MigrationTools.Clients.TfsObjectModel/Processors/TfsExportProfilePictureFromADProcessor.cs
optionsClassFile: /src/MigrationTools.Clients.TfsObjectModel/Processors/TfsExportProfilePictureFromADProcessorOptions.cs

redirectFrom:
- /Reference/Processors/TfsExportProfilePictureFromADProcessorOptions/
layout: reference
toc: true
permalink: /Reference/Processors/TfsExportProfilePictureFromADProcessor/
title: TfsExportProfilePictureFromADProcessor
categories:
- Processors
- 
topics:
- topic: notes
  path: /docs/Reference/Processors/TfsExportProfilePictureFromADProcessor-notes.md
  exists: false
  markdown: ''
- topic: introduction
  path: /docs/Reference/Processors/TfsExportProfilePictureFromADProcessor-introduction.md
  exists: false
  markdown: ''

---