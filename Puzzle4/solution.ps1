
filter Where-DigitsNeverDecrease {
    # All digits are greater than or equal to the last
    $stringArray = $_.ToString()
    for ($i = 0; $i -lt $stringArray.Length - 1; $i++) {
        if ([int]$stringArray[$i + 1] -lt [int]$stringArray[$i]) {
            return $null
        }
    }
    $_
}

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
    $stringArray = $_.ToString()
    [char] $prev = $null
    [int] $counter = 0
    for ($i = 0; $i -lt $stringArray.Length; $i++) {
        $cur = $stringArray[$i]
        if ($cur -eq $prev) {
            $counter++
        } else {
            if ($counter -eq 2) {
                return $_
            }
            $counter = 1
        }
        $prev = $cur
    }

    if ($counter -eq 2) {
        return $_
    }
}

307237..769058 |
    Where-HasLength 6 |
    Where-ContainsDouble |
    Where-DigitsNeverDecrease |
    Measure-Object

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
            12224 | Where-ContainsDouble | Should Be $null
            111122 | Where-ContainsDouble | Should Be 111122
        }
    }

    Context 'Sequential digits never decrease' {
        It 'must only contain digits that are equal to or greater than the previous' {
            123 | Where-DigitsNeverDecrease | Should Be 123
            122 | Where-DigitsNeverDecrease | Should Be 122
            121 | Where-DigitsNeverDecrease | Should Be $null
        }
    }
}

Invoke-Pester