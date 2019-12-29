<#
307237..769058 |
    Where-HasLength 6 |
    Where-Object {
        # Two adjacent digits are equal
        $found = $false
        $stringArray = $_.ToString()
        for ($i = 0; $i -lt $stringArray.Length - 1; $i++) {
            if ($stringArray[$i + 1] -eq $stringArray[$i]) {
                $found = $true
                break
            }
        }
        $found
    } |
    Where-Object {
        # All digits are greater than or equal to the last
        $correct = $true
        $stringArray = $_.ToString()
        for ($i = 0; $i -lt $stringArray.Length - 1; $i++) {
            if ([int]$stringArray[$i + 1] -lt [int]$stringArray[$i]) {
                $correct = $false
                break
            }
        }
        $correct
    } |
    Measure-Object
    #>
filter Where-HasLength {
    param(
        [int] $Length
    )
    if( $_.ToString().Length -eq $Length) {
        $_
    }
}

filter Where-ContainsDouble {
    # Two adjacent digits are equal
    $found = $false
    $stringArray = $_.ToString()
    for ($i = 0; $i -lt $stringArray.Length - 1; $i++) {
        if ($stringArray[$i + 1] -eq $stringArray[$i]) {
            return $_
        }
    }
}

Describe 'Matching number rules' {
    Context 'PIN length' {
        It 'must be 6 digits' {
            12 | Where-HasLength 3 | Should Be $null
            123 | Where-HasLength 3 | Should Be 123
            1234 | Where-HasLength 3 | Should Be $null
            12,123,1234 | Where-HasLength 3 | Should Be 123
        }
    }

    Context 'Duplicated digits' {
        It 'must have contain a double (e.g. 12234)' {
            123 | Where-ContainsDouble | Should Be $null
            1223 | Where-ContainsDouble | Should Be 1223
            1233 | Where-ContainsDouble | Should Be 1233
            1234 | Where-ContainsDouble | Should Be $null
        }
    }
}

Invoke-Pester