# TV Programme Scheduling System
## AVL Tree Design

## 1. Overview

This document contains the pseudocode design for the TV scheduling system.
The system is designed to manage broadcast schedules for multiple television channels. 
A custom AVL Tree is used to store and organise programmes efficiently.

The AVL Tree guarantees O(log n) insertion, deletion, and search operations. 
Programmes are ordered first by Channel, and then by StartTime.

## 2. Programme Structure

STRUCT Programme
    INTEGER ProgrammeID
    STRING Title
    STRING Channel
    DATETIME StartTime
    DATETIME EndTime
    STRING Genre
END STRUCT

StartTime is used as part of the ordering key.