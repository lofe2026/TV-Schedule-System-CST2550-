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

## 3. AVL Node Structure

STRUCT Node
    Programme data
    Node left
    Node right
    INTEGER height
END STRUCT

Each node stores one Programme and maintains height information for balancing.

## 4. Comparison Function

FUNCTION Compare(p1, p2)

    IF p1.Channel < p2.Channel
        RETURN -1

    IF p1.Channel > p2.Channel
        RETURN 1

    IF p1.StartTime < p2.StartTime
        RETURN -1

    IF p1.StartTime > p2.StartTime
        RETURN 1

    RETURN 0

END FUNCTION

Programmes are grouped by Channel and sorted by StartTime within each channel.