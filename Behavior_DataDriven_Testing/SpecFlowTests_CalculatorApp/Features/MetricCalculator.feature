Feature: Metric Calculator
Users can convert between metrics using the Calculator Web app:
https://js-calculator.nakov.repl.co from the [Metric Calculator] tab.

Scenario: Convert meters to sentimeters
	Given the input value is 6.8
	And initial metric is "m"
	And to second metric is "cm"
	When the conversion is performed
	Then the result should be 680

Scenario: Convert meters to milimeters
	Given the input value is 2.9
	And initial metric is "m"
	And to second metric is "mm"
	When the conversion is performed
	Then the result should be 2900

Scenario: Convert meters to kilometers
	Given the input value is 9000
	And initial metric is "m"
	And to second metric is "km"
	When the conversion is performed
	Then the result should be 9

Scenario: Convert centimeters to kilometers
	Given the input value is 10000
	And initial metric is "cm"
	And to second metric is "km"
	When the conversion is performed
	Then the result should be 0.1

Scenario: Convert centimeters to milimeters
	Given the input value is 220
	And initial metric is "cm"
	And to second metric is "mm"
	When the conversion is performed
	Then the result should be 2200

Scenario: Convert centimeters to meters
	Given the input value is 500
	And initial metric is "cm"
	And to second metric is "m"
	When the conversion is performed
	Then the result should be 5

Scenario: Convert kilometers to meters
	Given the input value is 5
	And initial metric is "km"
	And to second metric is "m"
	When the conversion is performed
	Then the result should be 5000

Scenario: Convert kilometers to centimeters
	Given the input value is 5.8
	And initial metric is "km"
	And to second metric is "cm"
	When the conversion is performed
	Then the result should be 580000

Scenario: Convert kilometers to milimeters
	Given the input value is 2.2
	And initial metric is "km"
	And to second metric is "mm"
	When the conversion is performed
	Then the result should be 2200000

Scenario: Convert milimeters to centimeters
	Given the input value is 5
	And initial metric is "mm"
	And to second metric is "cm"
	When the conversion is performed
	Then the result should be 0.5

Scenario: Convert milimeters to kilometers
	Given the input value is 100000
	And initial metric is "mm"
	And to second metric is "km"
	When the conversion is performed
	Then the result should be 0.1

Scenario: Convert milimeters to meters
	Given the input value is 8.8
	And initial metric is "mm"
	And to second metric is "m"
	When the conversion is performed
	Then the result should be 0.0088