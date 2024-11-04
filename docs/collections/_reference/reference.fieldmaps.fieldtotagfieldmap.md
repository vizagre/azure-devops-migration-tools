---
optionsClassName: FieldToTagFieldMapOptions
optionsClassFullName: MigrationTools.Tools.FieldToTagFieldMapOptions
configurationSamples:
- name: defaults
  order: 2
  description: 
  code: >-
    {
      "MigrationTools": {
        "Version": "16.0",
        "CommonTools": {
          "FieldMappingTool": {
            "FieldMaps": [
              {
                "FieldMapType": "FieldToTagFieldMap",
                "ApplyTo": [
                  "*"
                ]
              }
            ]
          }
        }
      }
    }
  sampleFor: MigrationTools.Tools.FieldToTagFieldMapOptions
- name: sample
  order: 1
  description: 
  code: >-
    {
      "MigrationTools": {
        "Version": "16.0",
        "CommonTools": {
          "FieldMappingTool": {
            "FieldMaps": [
              {
                "FieldMapType": "FieldToTagFieldMap",
                "ApplyTo": [
                  "SomeWorkItemType"
                ],
                "formatExpression": "{0} <br/><br/><h3>Acceptance Criteria</h3>{1}",
                "sourceFields": [
                  "System.Description",
                  "Microsoft.VSTS.Common.AcceptanceCriteria"
                ],
                "targetField": "System.Description"
              }
            ]
          }
        }
      }
    }
  sampleFor: MigrationTools.Tools.FieldToTagFieldMapOptions
- name: classic
  order: 3
  description: 
  code: >-
    {
      "$type": "FieldToTagFieldMapOptions",
      "sourceField": null,
      "formatExpression": "{0} <br/><br/><h3>Acceptance Criteria</h3>{1}",
      "ApplyTo": [
        "*",
        "SomeWorkItemType"
      ]
    }
  sampleFor: MigrationTools.Tools.FieldToTagFieldMapOptions
description: missing XML code comments
className: FieldToTagFieldMap
typeName: FieldMaps
architecture: 
options:
- parameterName: ApplyTo
  type: List
  description: missing XML code comments
  defaultValue: missing XML code comments
- parameterName: formatExpression
  type: String
  description: missing XML code comments
  defaultValue: missing XML code comments
- parameterName: sourceField
  type: String
  description: missing XML code comments
  defaultValue: missing XML code comments
status: missing XML code comments
processingTarget: missing XML code comments
classFile: /src/MigrationTools.Clients.TfsObjectModel/Tools/FieldMappingTool/FieldMaps/FieldToTagFieldMap.cs
optionsClassFile: ''

redirectFrom:
- /Reference/FieldMaps/FieldToTagFieldMapOptions/
layout: reference
toc: true
permalink: /Reference/FieldMaps/FieldToTagFieldMap/
title: FieldToTagFieldMap
categories:
- FieldMaps
- 
topics:
- topic: notes
  path: /docs/Reference/FieldMaps/FieldToTagFieldMap-notes.md
  exists: false
  markdown: ''
- topic: introduction
  path: /docs/Reference/FieldMaps/FieldToTagFieldMap-introduction.md
  exists: false
  markdown: ''

---