Get-Content (Join-Path $PSScriptRoot 'input.txt') | 
    %{ $_ / 3 } | 
    %{ [Math]::Floor($_) } | 
    %{ $_ - 2 } | 
    measure -Sum