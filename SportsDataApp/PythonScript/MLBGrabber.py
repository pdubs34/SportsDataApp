import requests
from bs4 import BeautifulSoup
import time
import csv

mlb_team_abbreviations = [
    "ARI", "ATL", "BAL", "BOS", "CHW", "CHC", "CIN", "CLE", "COL", "DET",
    "HOU", "KCR", "LAA", "LAD", "MIA", "MIL", "MIN", "NYY", "NYM", "OAK",
    "PHI", "PIT", "SDP", "SFG", "SEA", "STL", "TBR", "TEX", "TOR", "WSN"
]
mlb_team_names = {
    "ARI": "Arizona Diamondbacks",
    "ATL": "Atlanta Braves",
    "BAL": "Baltimore Orioles",
    "BOS": "Boston Red Sox",
    "CHW": "Chicago White Sox",
    "CHC": "Chicago Cubs",
    "CIN": "Cincinnati Reds",
    "CLE": "Cleveland Guardians",
    "COL": "Colorado Rockies",
    "DET": "Detroit Tigers",
    "HOU": "Houston Astros",
    "KCR": "Kansas City Royals",
    "LAA": "Los Angeles Angels",
    "LAD": "Los Angeles Dodgers",
    "MIA": "Miami Marlins",
    "MIL": "Milwaukee Brewers",
    "MIN": "Minnesota Twins",
    "NYY": "New York Yankees",
    "NYM": "New York Mets",
    "OAK": "Oakland Athletics",
    "PHI": "Philadelphia Phillies",
    "PIT": "Pittsburgh Pirates",
    "SDP": "San Diego Padres",
    "SFG": "San Francisco Giants",
    "SEA": "Seattle Mariners",
    "STL": "St. Louis Cardinals",
    "TBR": "Tampa Bay Rays",
    "TEX": "Texas Rangers",
    "TOR": "Toronto Blue Jays",
    "WSN": "Washington Nationals"
}
AllData = []
for team in mlb_team_abbreviations:
    url = f'https://www.baseball-reference.com/teams/{team}/'

# Fetch the web page's content
    response = requests.get(url)
    time.sleep(5)

# Check if the request was successful
    if response.status_code == 200:
        # Parse the HTML content
        soup = BeautifulSoup(response.text, 'html.parser')
        table_element = soup.find('table')
        if table_element:
            tr_elements = table_element.find_all('tr')
            for tr in tr_elements:
                list = []
                th_element = tr.find('th').text
                list.append(th_element)
                td_elements = tr.find_all('td')
                for td in td_elements:
                    if( td["data-stat"] == "W"):
                        list.append(td.text)
                    if( td["data-stat"] == "L"):
                        list.append(td.text)
                    if(team not in list):
                        list.append(team)
                    if(td["data-stat"] == "playoffs"):
                        if(td.text != ''):
                            list.append(td.text[:1])
                        else:
                            list.append("No")
                    if( td["data-stat"] == "win_loss_perc"):
                        list.append(td.text)
                AllData.append(list)
        else:
            print("Table with id='franchise_years' not found.")
    else:
        print(f"Request failed with status code {response.status_code}")
csv_file_path = "seasons.csv"
with open(csv_file_path, mode='w', newline='') as file:
    writer = csv.writer(file)
    for line in AllData:
        writer.writerow(line)
    file.close()

csv_file_path = "teams.csv"
with open(csv_file_path, mode='w', newline='') as file:
    writer = csv.writer(file)
    for team in mlb_team_names:
        list = (team,mlb_team_names[team])
        writer.writerow(list)
    file.close()



