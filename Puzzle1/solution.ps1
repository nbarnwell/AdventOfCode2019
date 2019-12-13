
function CalculateFuel {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory, ValueFromPipeline)]
        [int[]] $Mass
    )
    process {
        $Mass |
            ForEach-Object{ $_ / 3 } | 
            ForEach-Object{ [Math]::Floor($_) } | 
            ForEach-Object{ $_ - 2 } | 
            ForEach-Object { [Math]::Max($_, 0) } |
            Measure-Object -Sum |
            Select-Object -exp Sum
    }
}

Get-Content (Join-Path $PSScriptRoot 'input.txt') | 
    CalculateFuel |
    ForEach-Object{
        $moduleFuel = $_
        $additionalFuel = $moduleFuel
        do {
            $additionalFuel = CalculateFuel $additionalFuel
            $moduleFuel += $additionalFuel
        } while ($additionalFuel -gt 0)

        $moduleFuel
    } |
    Measure-Object -Sum