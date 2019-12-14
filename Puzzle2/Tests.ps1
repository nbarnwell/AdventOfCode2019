function Resize-Array([int[]] $InitialArray, [int[]] $Pointers) {
    $requiredSize = 1 + (Select-Max $Pointers)
    if ($InitialArray.Length -lt $requiredSize) {
        $newArray = [int[]]::new($requiredSize)
        [array]::Copy($InitialArray, $newArray, $InitialArray.Length)
        $newArray
    } else {
        $InitialArray
    }
}

function Add([int[]] $Registers, [int] $Pointer) {
    $addendPos1 = $Registers[$Pointer + 1]
    $addendPos2 = $Registers[$Pointer + 2]
    $resultPos = $Registers[$Pointer + 3]

    $Registers = Resize-Array $Registers $addendPos1,$addendPos2,$resultPos

    $Registers[$resultPos] = $Registers[$addendPos1] + $Registers[$addendPos2]

    $Registers
}

function Multiply([int[]] $Registers, [int] $Pointer) {
    $factorPos1 = $Registers[$Pointer + 1]
    $factorPos2 = $Registers[$Pointer + 2]
    $resultPos = $Registers[$Pointer + 3]

    $Registers = Resize-Array $Registers $factorPos1,$factorPos2,$resultPos

    $Registers[$resultPos] = $Registers[$factorPos1] * $Registers[$factorPos2]

    $Registers
}

function Select-Max([Parameter(Mandatory)] [int[]] $Numbers) {
    $Numbers | Sort-Object -Descending | Select-Object -First 1
}

function ParseProgram([string] $Program) {
    $registers = $Program.Split(',') | ForEach-Object { $_ -as [int] }
    $registers
}

function Intcode([string] $Program) {
    $registers = ParseProgram $Program
    $pointer = 0;

    $opcode = $registers[$pointer]

    while ($opcode -ne 99) {
        switch ($opcode) {
            1 { 
                $registers = Add $registers $pointer 
                $pointer += 4
            }
            2 { 
                $registers = Multiply $registers $pointer 
                $pointer += 4
            }
            99 { 
                return 
            }
            Default { throw "Invalid opcode $opcode and position $pointer" }
        }
        $opcode = $registers[$pointer]
    }

    [string]::Join(',', $registers)
}

Describe 'Maths' {
    Context 'Single number' {
        It 'Should return value' {
            Select-Max @(1) | Should Be 1
        }
    }
    Context 'Multiple numbers' {
        It 'Should return highest value' {
            Select-Max @(1, 2) | Should Be 2
        }
    }
}

Describe 'Parse program' {
    Context 'Simple array, no whitespace' {
        It 'Returns array for 1,2,3' {
            ParseProgram '1,2,3' | Should Be 1,2,3
        }
    }
}

Describe 'Intcode' {
    Context 'Single instruction' {
        It 'Adds 1 and 1' {
            Intcode '1,0,0,0,99' | Should Be '2,0,0,0,99'
        }
        It 'Multiplies 3 and 2' {
            Intcode '2,3,0,3,99' | Should Be '2,3,0,6,99'
        }
    }

    Context 'Multiple instructions' {
        It 'Adds 9 and 2, then 4 and 5' {
            Intcode '1,1,1,4,99,5,6,0,99' | Should Be '30,1,1,4,2,5,6,0,99'
        }
    }
}

Invoke-Pester

function Main {
    # Load program
    $program = (Get-Content 'input.txt') | Select-Object -First 1

    # Modify program
    $registers = ParseProgram $program
    $registers[1] = 12
    $registers[2] = 2
    $program = [string]::Join(',', $registers)

    # Execute program
    $result = Intcode $program
    $registers = ParseProgram $result
    $registers[0]
}

Main